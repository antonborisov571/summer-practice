/**
 * Тип профиля
 */
export type Profile = {
  /**
   * Имя пользователя
   */
  firstName: string;

  /**
   * Фамилия пользователя
   */
  lastName: string;

  /**
   * День рождения пользователя
   */
  birthday: Date;

  /**
   * Дата регистрации пользователя
   */
  dateRegistration: Date;

  /**
   * Ссылка на аватар пользователя
   */
  avatar: string | null;

  /**
   * Почта пользователя
   */
  email: string;

  /**
   * Включена ли двухфакторка
   */
  twoFactorEnabled: boolean;
};


/**
 * Имя + фамилия
 */
export const getProfileName = (profile: Profile) =>
  profile.firstName + " " + profile.lastName;
