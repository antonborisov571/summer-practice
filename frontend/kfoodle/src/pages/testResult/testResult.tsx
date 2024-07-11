import classes from "./styles/testResult.module.css"
import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {api} from "../../config/axios.ts";

interface GetTestResultResponse {
  score: number;
  title: string;
  numQuestions: number;
  numCorrectAnswers: number;
  startTime: Date;
  endTime: Date;
}

function TestResult() {

  const { id } = useParams<{ id: string }>();
  const [testResult, setTestResult] = useState<GetTestResultResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api
      .get(`test/${id}/GetTestResult`)
      .then((response) => {
        console.log(response.data)
        setTestResult(response.data);
        setLoading(false);
      })
      .catch((error) => {
        setError("Error fetching test result");
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!testResult) {
    return <div>No test result found</div>;
  }

  return (
    <div className={classes.testResultWrapper}>
      <div className={classes.testResultCard}>
        <h2>Результаты тестирования</h2>
        <p><strong>{testResult.title}</strong></p>
        <p><strong>Число баллов:</strong> {testResult.score}/{testResult.numQuestions}</p>
        <p><strong>Количество верно отвеченных вопросов:</strong> {testResult.numCorrectAnswers}/{testResult.numQuestions}</p>
        <p><strong>Начало тестирования:</strong> {new Date(testResult.startTime).toLocaleString()}</p>
        <p><strong>Конец тестирования:</strong> {new Date(testResult.endTime).toLocaleString()}</p>
      </div>
    </div>
  );
}


export default TestResult;