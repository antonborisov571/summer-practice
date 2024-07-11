import classes from "./callToActionTemplate.module.css";
import Button from "../../../../components/general/button/button.tsx";
import { Link } from "react-router-dom";

function CallToActionTemplate(props: { data: CallToActionData }) {
  return (
    <div className={classes.templateWrapper}>
      <img src={props.data.imageSrc} />
      <div className={classes.rightPart}>
        <h2>{props.data.title}</h2>
        <p>{props.data.text}</p>
      </div>
    </div>
  );
}

export default CallToActionTemplate;

export type CallToActionData = {
  imageSrc: string;
  title: string;
  text: string;
  buttonLabel: string;
  redirectTo: string;
};
