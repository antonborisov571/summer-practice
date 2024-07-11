import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { api } from "../../config/axios.ts";
import classes from "./styles/userResults.module.css";
import { format } from "date-fns";

interface UserResult {
  firstName: string;
  lastName: string;
  score: number;
  startTime: Date;
  endTime: Date;
}

interface GetUserResultsResponse {
  title: string;
  userResults: UserResult[];
}

function UserResults() {
  const { id } = useParams<{ id: string }>();
  const [userResults, setUserResults] = useState<UserResult[]>([]);
  const [title, setTitle] = useState<string>("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api
      .get<GetUserResultsResponse>(`test/${id}/GetUserResults`)
      .then((response) => {
        setUserResults(response.data.userResults);
        setTitle(response.data.title);
        setLoading(false);
      })
      .catch((error) => {
        setError("Error fetching user results");
        setLoading(false);
      });
  }, [id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className={classes.userResultsWrapper}>
      <h1>Результаты теста: {title}</h1>
      <ul>
        {userResults.map((result, index) => (
          <li key={index} className={classes.resultCard}>
            <h2>{result.firstName} {result.lastName}</h2>
            <p><strong>Число баллов:</strong> {result.score}</p>
            <p><strong>Начало тестирования:</strong> {format(new Date(result.startTime), 'dd/MM/yyyy HH:mm')}</p>
            <p><strong>Конец тестирования:</strong> {format(new Date(result.endTime), 'dd/MM/yyyy HH:mm')}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default UserResults;