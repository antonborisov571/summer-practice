import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { api } from "../../../../config/axios.ts";
import { Question, QuestionType } from "../../../../hooks/types/Question.ts";
import { BeatLoader } from "react-spinners";
import { EditTask } from "../editTasks";
import ButtonGoBack from "../../../../components/general/buttonGoBack/buttonGoBack";
import classes from "./index.module.css";
import { Test } from "../../../../hooks/types/Test.ts";
import TrashButton from "../../../../components/general/trashButton";
import Button from "../../../../components/general/button/button";
import NotFound from "../../../../components/general/responses/notFound/notFound.tsx";
import Forbid from "../../../../components/general/responses/forbid/forbid.jsx";

function EditTasksBlockComponent() {
  const navigator = useNavigate();
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [currentTest, setCurrentTest] = useState<
    Test | undefined | null
  >();



  const notFound = <NotFound></NotFound>;
  const [isForbid, setIsForbid] = useState(false);
  const forbid = <Forbid></Forbid>;

  useEffect(() => {
    setLoading(true);
    api
      .get(`test/${id}/GetEditQuestions`)
      .then((response) => {
        setCurrentTest(response.data)
        setLoading(false)
      })
      .catch((error) => {
        if (error.response?.status === 403) setIsForbid(true);
      });
  }, []);

  if (isForbid) return forbid;

  if (loading) return <BeatLoader />;

  if (!loading && currentTest === null) return notFound;

  const handleChange = (task: Question) => {
    console.log(task);

    const taskIndex = currentTest!.questions.findIndex((t) => task.id == t.id);
    const newTasks = [...currentTest!.questions];
    newTasks[taskIndex] = task;

    const newCurrentBlock = {
      ...currentTest!,
      questions: newTasks,
    };

    console.log(newCurrentBlock);
    setCurrentTest(newCurrentBlock);
  };

  const saveChanges = () => {
    api
      .patch("test/UpdateQuestions", currentTest)
      .catch((res) => console.log(res));
  };

  const addTask = () => {
    const newTask: Question = {
      id:
        currentTest!.questions.length > 0
          ? currentTest!.questions[currentTest!.questions.length - 1].id + 1
          : 1,
      questionText: "",
      questionType: QuestionType.InputAnswer,
      rightAnswer: "",
      choices: []
    };
    const newQuestions = [...currentTest!.questions, newTask];

    const newCurrentBlock = {
      ...currentTest!,
      questions: newQuestions,
    };

    console.log(newCurrentBlock);
    setCurrentTest(newCurrentBlock);
  };

  const removeTask = (taskId: number) => {
    const newQuestions = currentTest!.questions.filter((t) => taskId != t.id);

    const newCurrentBlock = {
      ...currentTest!,
      questions: newQuestions,
    };

    console.log(newCurrentBlock);
    setCurrentTest(newCurrentBlock);
  };

  return (
    <div className={classes.container}>
      <ButtonGoBack
        onClick={() => navigator(`/editTest/${id}`)}
      >
        ⟵ к редактированию теста
      </ButtonGoBack>
      <h2 className={classes.title}>{currentTest?.title}</h2>
      {currentTest!.questions.map((question, index) => (
        <div key={question.id} className={classes.containerItem}>
          <span className={classes.taskNumber}>{index + 1}.</span>
          <TrashButton
            onClick={(event: any) => {
              event.preventDefault();
              removeTask(question.id);
            }}
          />
          <EditTask task={question} onChange={handleChange} />
        </div>
      ))}
      <div className={classes.containerItem}>
        <Button onClick={saveChanges} style={{ width: "100%" }}>
          Сохранить изменения
        </Button>
        <Button onClick={addTask} style={{ width: "100%" }}>
          Добавить задание
        </Button>
      </div>
    </div>
  );
}

export default EditTasksBlockComponent;
