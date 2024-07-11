import {Link, useNavigate} from "react-router-dom";
import Logo from "../../assets/kfoodle.svg";
import classes from "./header.module.css";
import { useProfile } from "../../hooks/profile/useProfile";
import { getProfileName } from "../../hooks/profile/profile";
import Avatar from "react-avatar";
import CreateTestIcon from "../../assets/header/publishTrip.svg"
import {api} from "../../config/axios.ts";

/**
 * Компонент header-а
 */
function Header() {
  const [profile, _] = useProfile();
  const navigator = useNavigate();

  const createTest = () => {
    api
      .post("test/CreateTest")
      .then(response => {
        console.log(response.data);
        navigator(`editTest/${response.data.testId}`)
      })
      .catch()
  }

  return (
    <header className={classes.header}>
      <div className={classes.leftWrapper}>
        <Link to="/" className={classes.logoContainer}>
          <img src={Logo}></img>
        </Link>
        {profile == null ? "" :
          <div className={classes.links}>
            <div onClick={createTest} className={classes.link}>
              <img style={{height: "25px", marginRight: "10px"}} src={CreateTestIcon}></img>
              Создать тест
            </div>
            <div onClick={() => navigator("/catalog")} className={classes.link}>
              Доступные тесты
            </div>
          </div>
        }
      </div>
      <div className={classes.rightWrapper}>
        {profile == null ? (
          <Link to="/login" className={classes.profileContainer}>
            Войти
          </Link>
        ) : (
          <Link to="/profile" className={classes.profileContainer}>
            <p className={classes.profileName}>{getProfileName(profile)}</p>
            <Avatar
              name={getProfileName(profile)}
              size="50"
              className={classes.profileAvatar}
              src={profile.avatar ? `data:image;base64,${profile.avatar}` : undefined}
            ></Avatar>
          </Link>
        )}
      </div>
    </header>
  );
}

export default Header;
