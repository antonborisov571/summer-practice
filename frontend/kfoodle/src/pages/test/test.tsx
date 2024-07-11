import classes from "./styles/test.module.css";
import { useProfile } from "../../hooks/profile/useProfile.ts";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { api } from "../../config/axios.ts";
import { BeatLoader } from "react-spinners";
import InputForm from "../../components/general/inputform/inputform.tsx";
import { format, differenceInSeconds, addMinutes } from "date-fns";
import Button from "../../components/general/button/button.tsx";
import {
  InputAnswersQuestion,
  MultipleAnswersQuestion,
  QuestionType,
  SingleAnswerQuestion,
} from "../../hooks/types/Question.ts";
import {
  SingleAnswerTaskExtension,
  MultipleAnswersTaskExtension,
  InputAnswersTaskExtension,
} from "./components/answerExtensions.tsx";

export type GetTestResponse = {
  id: string;
  isAccess: boolean;
  title: string;
  description?: string;
  endDate?: Date;
  startTime: Date;
  duration?: number;
  numQuestions?: number;
  maxAttempts?: number;
  questions: QuestionItem[];
  testAttemptId: number;
};

export type QuestionItem = {
  id: string;
  questionText: string;
  questionType: QuestionType;
  answer: string;
  choices: ChoiceItem[];
};

export type ChoiceItem = {
  id: string;
  text: string;
  isSelected: boolean;
};

function Test() {
  const [profile, loadingProfile] = useProfile();
  const { id } = useParams();
  const navigate = useNavigate();
  const [test, setTest] = useState<GetTestResponse>();
  const [loading, setLoading] = useState(true);
  const [timeLeft, setTimeLeft] = useState<number | null>(null);

  const answerExtensions = {
    [QuestionType.SingleAnswer]: (question) => (
      <SingleAnswerTaskExtension
        id="answer"
        task={question}
        onChange={handleChange}
        testAttemptId={test.testAttemptId}
      />
    ),
    [QuestionType.MultipleAnswers]: (question) => (
      <MultipleAnswersTaskExtension
        id="answer"
        task={question}
        onChange={handleChange}
        testAttemptId={test.testAttemptId}
      />
    ),
    [QuestionType.InputAnswer]: (question) => (
      <InputAnswersTaskExtension
        id="answer"
        task={question}
        onChange={handleChange}
        testAttemptId={test.testAttemptId}
      />
    ),
    [QuestionType.SequenceAnswer]: null,
  };

  const handleChange = (questionId, updatedData) => {
    setTest((prevTest) => {
      const updatedQuestions = prevTest.questions.map((question) => {
        if (question.id === questionId) {
          if (Array.isArray(question.choices)) {
            return { ...question, choices: updatedData };
          } else {
            return { ...question, answer: updatedData };
          }
        }
        return question;
      });
      return { ...prevTest, questions: updatedQuestions };
    });
  };

  useEffect(() => {
    setLoading(true);
    api
      .get(`test/${id}`)
      .then((response) => {
        setTest(response.data);
        if (response.data.isAccess && response.data.duration) {
          const endTime = addMinutes(new Date(response.data.startTime), response.data.duration);
          setTimeLeft(differenceInSeconds(endTime, new Date()));
        }
        setLoading(false);
      })
      .catch();
  }, [id]);

  useEffect(() => {
    if (timeLeft !== null) {
      const timer = setInterval(() => {
        setTimeLeft((prevTime) => {
          if (prevTime && prevTime > 0) {
            return prevTime - 1;
          } else {
            clearInterval(timer);
            endTest();
            return 0;
          }
        });
      }, 1000);
      return () => clearInterval(timer);
    }
  }, [timeLeft]);

  const formatTime = (seconds) => {
    const mins = Math.floor(seconds / 60);
    const secs = seconds % 60;
    return `${mins}:${secs < 10 ? "0" : ""}${secs}`;
  };

  const startTest = () => {
    api
      .get(`test/${id}/StartTest`)
      .then((response) => {
        setTest(response.data);
        const endTime = addMinutes(new Date(response.data.startTime), response.data.duration);
        setTimeLeft(differenceInSeconds(endTime, new Date()));
      })
      .catch();
  };

  const endTest = () => {
    api
      .post(`test/${id}/EndTest`, { testId: test.id, testAttemptId: test.testAttemptId })
      .then(() => {
        navigate(`/testResult/${test.testAttemptId}`);
      })
      .catch();
  };

  if (profile == null && !loadingProfile) return <Navigate to="/login"></Navigate>;
  if (loading) return <BeatLoader />;

  return (
    <div className={classes.createTestWrapper}>
      <div className={classes.testTitleWrapper}>
        <div>
          <ul className={classes.inputsWrapper}>
            <div className={classes.inputWrapper}>
              <h3>Наименование</h3>
              <div>{test.title}</div>
            </div>
            <div className={classes.inputWrapper}>
              <h3>Описание</h3>
              <p style={{ maxWidth: "900px", wordWrap: "break-word" }}>{test.description}</p>
            </div>
            <div className={classes.inputTimesWrapper}>
              <div className={classes.inputWrapper}>
                <h3>Время завершения</h3>
                <div>{test.endDate ? format(new Date(test.endDate), "yyyy-MM-dd") : "Нет времени завершения"}</div>
              </div>
              <div className={classes.inputWrapper}>
                <h3>Время на прохождение</h3>
                <div>{test.duration ? `${test.duration} минут` : "Неограничено"}</div>
              </div>
              {test.isAccess && (
                <div className={classes.inputWrapper}>
                  <h3>Оставшееся время</h3>
                  <div className={classes.countdown}>{timeLeft !== null ? formatTime(timeLeft) : "Неограничено"}</div>
                </div>
              )}
            </div>
            <div className={classes.inputWrapper}>
              <h3>Максимальное число попыток</h3>
              <div>{test.maxAttempts}</div>
            </div>
          </ul>
        </div>
      </div>
      {test.isAccess ? (
        <ul className={classes.questionsWrapper}>
          {test.questions.map((question) => (
            <div key={question.id} className={classes.questionWrapper}>
              <h3>{question.questionText}</h3>
              {question.questionType !== null && answerExtensions[question.questionType] && (
                <>
                  <label className={classes.label} htmlFor="answer">
                    Выберите или введите ответ
                  </label>
                  {answerExtensions[question.questionType](question)}
                </>
              )}
            </div>
          ))}
          <Button onClick={endTest}>Завершить тестирование</Button>
        </ul>
      ) : (
        <div>
          <Button onClick={startTest}>Начать тестирование</Button>
        </div>
      )}
    </div>
  );
}

export default Test;