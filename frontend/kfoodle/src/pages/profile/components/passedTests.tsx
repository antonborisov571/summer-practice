import classes from "./styles/trips.module.css";
import {useEffect, useState} from "react";
import {api} from "../../../config/axios.ts";
import {useNavigate} from "react-router-dom";

interface TestResult {
  score: number;
  title: string;
  numQuestions: number;
  numCorrectAnswers: number;
  startTime: Date;
  endTime: Date;
}

interface GetPassedTestsResponse {
  testResults: TestResult[];
}

function PassedTests() {
  const [tests, setTests] = useState<TestResult[]>([]);

  useEffect(() => {
    api
      .get<GetPassedTestsResponse>("test/GetPassedTests")
      .then((response) => {
        setTests(response.data.testResults);
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return (
    <div className={classes.tripsWrapper}>
      <h1>Ваши пройденные тесты</h1>
      <ul>
        {tests.map((test, index) => (
          <li key={index} className={classes.testCard}>
            <h2><strong>{test.title}</strong></h2>
            <p><strong>Число баллов:</strong> {test.score}/{test.numQuestions}</p>
            <p><strong>Количество верно отвеченных
              вопросов:</strong> {test.numCorrectAnswers}/{test.numQuestions}</p>
            <p><strong>Начало тестирования:</strong> {new Date(test.startTime).toLocaleString()}</p>
            <p><strong>Конец тестирования:</strong> {new Date(test.endTime).toLocaleString()}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default PassedTests;