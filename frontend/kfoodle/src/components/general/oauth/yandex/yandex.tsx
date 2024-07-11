import classes from "./yandex.module.css";
import YandexLogo from "../../../../assets/yandex.svg";

/**
 * Компонент кнопки для входа через Яндекс
 */
function Yandex() {
  return (
    <a className={classes.authYandex} href={"http://localhost:5201/api/oauth/ExternalLogin?provider=Yandex"}>
      <img style={{width:"30px"}} src={YandexLogo} />
      <div>Войти через Yandex</div>
    </a>
);
}

export default Yandex;
