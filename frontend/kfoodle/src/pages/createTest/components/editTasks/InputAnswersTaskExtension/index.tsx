import classes from "./index.module.css";
import { InputAnswersQuestion } from "../../../../../hooks/types/Question.ts";
import InputForm from "../../../../../components/general/inputform/inputform";

function InputAnswersTaskExtension({
  id,
  task,
  onChange,
}: {
  id: string;
  task: InputAnswersQuestion;
  onChange: (attr: string, value: any) => void;
}) {

  const updateAnswer = (description: string) => {
    onChange("rightAnswer", description);
  };

  return (
    <div id={id} className={classes.answersContainer}>
      <div className={classes.answer}>
        <InputForm
          style={{ width: "100%" }}
          value={task.rightAnswer}
          onChange={(event) =>
            updateAnswer(event.target.value)
          }
        />
      </div>
    </div>
  );
}

export default InputAnswersTaskExtension;
