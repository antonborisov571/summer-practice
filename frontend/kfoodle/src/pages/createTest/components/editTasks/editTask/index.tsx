import classes from "./index.module.css";
import {
  InputAnswersQuestion,
  MultipleAnswersQuestion,
  SingleAnswerQuestion,
  Question,
  QuestionType,
} from "../../../../../hooks/types/Question.ts";
import InputForm from "../../../../../components/general/inputform/inputform";
import SingleAnswerTaskExtension from "../SingleAnswerTaskExtension";
import MultipleAnswersTaskExtension from "../MultipleAnswersTaskExtension";
import InputAnswersTaskExtension from "../InputAnswersTaskExtension";
import Select from "../../../../../components/general/inputform/select";
import InputFormArea from "../../../../../components/general/inputform/inputFormArea";

function EditTask({
  task,
  onChange,
}: {
  task: Question;
  onChange: (task: Question) => void;
}) {
  const options = [
    { value: QuestionType.SingleAnswer, label: "Задание с выбором одного ответа" },
    {
      value: QuestionType.MultipleAnswers,
      label: "Задание с выбором многих ответов",
    },
    { value: QuestionType.SequenceAnswer, label: "Задание на указание в нужном порядке" },
    { value: QuestionType.InputAnswer, label: "Задание с текстовым ответом" },
  ];

  const answerExtensions = {
    [QuestionType.SingleAnswer]: () => (
      <SingleAnswerTaskExtension
        id="answer"
        task={task as SingleAnswerQuestion}
        onChange={handleChange}
      />
    ),
    [QuestionType.MultipleAnswers]: () => (
      <MultipleAnswersTaskExtension
        id="answer"
        task={task as MultipleAnswersQuestion}
        onChange={handleChange}
      />
    ),
    [QuestionType.InputAnswer]: () => (
      <InputAnswersTaskExtension
        id="answer"
        task={task as InputAnswersQuestion}
        onChange={handleChange}
      />
    ),
    [QuestionType.SequenceAnswer]: null,
  };

  const taskTypeConverters = {
    [QuestionType.SingleAnswer]: (previousTask: Question) =>
      ({
        id: previousTask.id,
        questionText: previousTask.questionText,
        questionType: QuestionType.SingleAnswer,
        choices: [
          { id: 0, choiceText: "", isCorrect: true },
          { id: 1, choiceText: "", isCorrect: false },
        ],
      }) as SingleAnswerQuestion,
    [QuestionType.MultipleAnswers]: (previousTask: Question) =>
      ({
        id: previousTask.id,
        questionText: previousTask.questionText,
        questionType: QuestionType.MultipleAnswers,
        choices: [
          { id: 0, choiceText: "", isCorrect: true },
          { id: 1, choiceText: "", isCorrect: false },
        ],
      }) as MultipleAnswersQuestion,
    [QuestionType.InputAnswer]: (previousTask: Question) =>
      ({
        id: previousTask.id,
        questionText: previousTask.questionText,
        questionType: QuestionType.InputAnswer,
        rightAnswer: ""
      }) as InputAnswersQuestion,
    [QuestionType.SequenceAnswer]: (previousTask: Question) =>
      ({
        id: previousTask.id,
        questionText: previousTask.questionText,
        questionType: QuestionType.SequenceAnswer,
      }) as Question,
  };

  const handleChange = (attr: string, value: any): void => {
    task = {
      ...task,
      [attr]: value,
    };
    onChange(task);
  };

  const setTaskType = (event) => {
    const taskType: QuestionType = Number.parseInt(event.target.value);
    onChange(taskTypeConverters[taskType](task) as Question);
  };

  return (
    <div className={classes.taskContainer}>
      <label className={classes.label} htmlFor="title">
        Текст задания
      </label>
      <InputFormArea
        id="title"
        style={{
          minHeight: "8rem",
          resize: "vertical",
        }}
        maxLength="280"
        value={task.questionText}
        onChange={(event) => handleChange("questionText", event.target.value)}
      />

      <label className={classes.label} htmlFor="questionType">
        Тип задания
      </label>
      <Select id="questionType" value={task.questionType} onChange={setTaskType}>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </Select>

      {task.questionType === null
        ? null
        : answerExtensions[task.questionType] && (
            <>
              <label className={classes.label} htmlFor="answer">
                Ответ
              </label>
              {answerExtensions[task.questionType]!()}
            </>
      )}
    </div>
  );
}

export default EditTask;
