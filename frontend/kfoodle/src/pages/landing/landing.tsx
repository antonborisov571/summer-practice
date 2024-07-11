import classes from "./landing.module.css";
import CallToActionTemplate, {
  CallToActionData,
} from "./components/callToActionTemplate/callToActionTemplate";
import StartLearning from "../../assets/startLearning.svg";
import StartTeaching from "../../assets/startTeaching.svg";
import Reviews from "./components/platformReviews/reviews";
import { useProfile } from "../../hooks/profile/useProfile";

function Landing() {
  const [profile, _] = useProfile();


  const callToStartLearningData: CallToActionData = {
    imageSrc: StartLearning,
    title: "Создавайте, проходите и анализируйте тесты с легкостью!",
    text: "Платформа управления тестами – это ваш универсальный инструмент для создания, прохождения и анализа тестов. Наше решение предназначено как для преподавателей и инструкторов, так и для студентов, которые хотят улучшить свои знания и навыки.",
    buttonLabel: "начать учиться",
    redirectTo: "/catalog",
  };

  const callToStartTeachingData: CallToActionData = {
    imageSrc: StartTeaching,
    title: "Станьте преподавателем",
    text: "Преподавайте из любой точки мира. Kfoodle предоставляет вам средства для создания тестов то, что вы любите.",
    buttonLabel: "начать преподавать",
    redirectTo: "/teaching",
  };

  return (
    <div className={classes.contentWrapper}>
      <CallToActionTemplate
        data={callToStartLearningData}
      ></CallToActionTemplate>
      <Reviews></Reviews>
      <CallToActionTemplate
        data={callToStartTeachingData}
      ></CallToActionTemplate>
    </div>
  );
}

export default Landing;

type CourseAuthor = {
  name: string;
};

type CourseCategory = {
  id: string;
  name: string;
};

export type CourseCard = {
  isGlowing: boolean;
  isOwned: boolean;
  pictureHref: string;
  title: string;
  price: number;
  id: string;
  category: CourseCategory;
  author: CourseAuthor;
};
