import classes from "./styles/profile.module.css";
import {useProfile} from "../../../hooks/profile/useProfile";
import InputForm from "../../../components/general/inputform/inputform.tsx";
import {ChangeEvent, useState} from "react";
import Button from "../../../components/general/button/button";
import Avatar from "react-avatar";
import {getProfileName, Profile} from "../../../hooks/profile/profile";
import {api} from "../../../config/axios";
import ProfileService from "../../../services/profileService";
import {Errors} from "./types/errors.tsx";
import { format } from 'date-fns';
import ruLocale from 'date-fns/locale/ru';
import WindowAvatar from "./info/windowAvatar.tsx";
import LoginService from "../../../services/loginService.tsx";
import {useNavigate} from "react-router-dom";
import Radio from "../../../components/general/radio/radio.tsx";

export enum TypePreference {
  Talk,
  Smoking,
  Music,
  Animal,
}

/**
 * Компонент с выводом инфорамации о пользователе
 */
function ProfileInfo() {
  const [originalProfile, _] = useProfile();
  const [profile, setProfile] = useState<Profile>({
    ...originalProfile,
  } as Profile);


  const [errors, setErrors] = useState<Errors>({
    firstName: null,
    lastName: null,
    avatar: null,
  });

  const [avatarChangeModalOpen, setAvatarChangeModalOpen] = useState(false);
  const [passwordSent, setPasswordSent] = useState<Boolean>(false);
  const [profileInfoSaved, setProfileInfoSaved] = useState(false);

  const navigate = useNavigate();

  const saveInfo = (e: any) => {
    e.preventDefault();setProfileInfoSaved
    setProfileInfoSaved(false);
    setErrors({
      firstName: null,
      lastName: null,
      avatar: null,
    });
    api
      .patch("account/UpdateUserInfo", {
        firstName: profile.firstName,
        lastName: profile.lastName,
      })
      .then((_) => {
        setProfileInfoSaved(true);
        ProfileService.fetchProfile();
      })
      .catch((error) => {
        if (error.response.status == 400) {
          if (error.response.data.errors.FirstName)
            setErrors({ ...errors, firstName: "Введите правильное имя" });
          if (error.response.data.errors.LastName)
            setErrors({ ...errors, lastName: "Введите правильную фамилию" });
        }
      });
  };

  const deleteAvatar = (e: any) => {
    e.preventDefault();
    setErrors({
      firstName: null,
      lastName: null,
      avatar: null,
    });
    api
      .put("account/UpdateUserInfo", {
        avatarHref: null,
      })
      .then((_) => {
        setAvatarChangeModalOpen(false);
        ProfileService.fetchProfile();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const sendPasswordReset = () => {
    setPasswordSent(false);
    api
      .post("auth/forgotPassword", {
        email: profile.email,
      })
      .then((_) => {
        setPasswordSent(true);
      })
      .catch();
  };

  const formatDate = (date: Date) => {
    return format(date, 'd MMMM yyyy', {
      // eslint-disable-next-line @typescript-eslint/ban-ts-comment
      // @ts-expect-error
      locale: ruLocale
    });
  };

  const exitProfile = () => {
    LoginService.clearTokens();
    navigate("/");
  };

  const updateTwoFactor = () => {
    api
      .patch("account/updateTwoFactor", {twoFactorEnabled: !profile.twoFactorEnabled})
      .catch(error => console.log(error));
    setProfile({...profile, twoFactorEnabled: !profile.twoFactorEnabled});
  };

  if (originalProfile == null) return <></>;

  return (
    <div className={classes.pageWrapper}>
      <h3 className={classes.pageTitle}>Редактирование профиля</h3>
      <div className={classes.profileInfoContents}>
        <form onSubmit={saveInfo}>
          <div className={classes.profileInfoGrid}>
            <label
              htmlFor="profile_first_name"
              className={classes.profileInfoLabel}
            >
              Имя
            </label>
            <div className={classes.profileInfoInputWrapper}>
              <InputForm
                id="profile_last_name"
                value={profile.firstName}
                onChange={(e: ChangeEvent<HTMLInputElement>) =>
                  setProfile({...profile, firstName: e.target.value})
                }
              ></InputForm>
              {errors.firstName ? (
                <div className={classes.profileInfoError}>
                  {errors.firstName}
                </div>
              ) : (
                <></>
              )}
            </div>
            <label
              htmlFor="profile_last_name"
              className={classes.profileInfoLabel}
            >
              Фамилия
            </label>
            <div className={classes.profileInfoInputWrapper}>
              <InputForm
                id="profile_last_name"
                value={profile.lastName}
                onChange={(e: ChangeEvent<HTMLInputElement>) =>
                  setProfile({...profile, lastName: e.target.value})
                }
              ></InputForm>
              {errors.lastName ? (
                <div className={classes.profileInfoError}>
                  {errors.lastName}
                </div>
              ) : (
                <></>
              )}
            </div>
          </div>
          <div className={classes.profileInfoFooter}>
            <div className={classes.profileInfoRightButton}>
              <Button type="submit">Сохранить</Button>
              {profileInfoSaved ? (
                <label className={classes.profileInfoSaved}>Сохранено</label>
              ) : (
                <></>
              )}
            </div>
          </div>
        </form>

        <hr className={classes.hr}></hr>

        <div className={classes.profileInfoGrid}>
          <label className={classes.pageTitle} htmlFor="profile_avatar">Аватар</label>
          <div className={classes.profileInfoInputWrapper}>
            <Avatar
              name={getProfileName(profile)}
              size="150"
              className={classes.profileAvatar}
              src={originalProfile.avatar ? `data:image;base64,${originalProfile.avatar}` : undefined}
            ></Avatar>
          </div>
        </div>
        <div className={classes.profileInfoFooter}>
          <div className={classes.profileInfoLeftButton}>
            <Button
              style={{width: "100%"}}
              onClick={() => setAvatarChangeModalOpen(true)}
            >
              Загрузить
            </Button>
          </div>
          <div className={classes.profileInfoRightButton}>
            <Button
              style={{width: "100%"}}
              disabled={!originalProfile.avatar}
              onClick={deleteAvatar}
            >
              Удалить
            </Button>
          </div>
        </div>

        <hr className={classes.hr}></hr>
        <p className={classes.changePasswordDesc}>
          Дата рождения: {formatDate(profile.birthday)}
        </p>
        <p className={classes.changePasswordDesc}>
          Дата регистрации: {formatDate(profile.dateRegistration)}
        </p>
        <hr className={classes.hr}></hr>
        <div>
          <h3 className={classes.pageTitle}>Пароль</h3>
          <p className={classes.changePasswordDesc}>
            На вашу почту будет отправлена ссылка для смены пароля.
          </p>
          <Button disabled={passwordSent} onClick={sendPasswordReset}>
            Cменить пароль
          </Button>
          {passwordSent ? (
            <p className={classes.changePasswordSuccess}>
              Письмо было отправлено на ваш электронный ящик.
            </p>
          ) : (
            <></>
          )}
        </div>
        <div style={{display:"flex", alignItems:"center"}}>
          <p>Двухфакторная авторизация</p>
          <Radio
            checked={profile.twoFactorEnabled ? "t" : ""}
            onChange={() => updateTwoFactor()}
          >
          </Radio>
        </div>
        <hr className={classes.hr}></hr>
        <div>
          <Button
            onClick={exitProfile}
          >
            Выйти
          </Button>
        </div>
      </div>
      <WindowAvatar
        errors={errors}
        setErrors={setErrors}
        avatarChangeModalOpen={avatarChangeModalOpen}
        setAvatarChangeModalOpen={setAvatarChangeModalOpen}
      ></WindowAvatar>
    </div>
  );
}

export default ProfileInfo;
