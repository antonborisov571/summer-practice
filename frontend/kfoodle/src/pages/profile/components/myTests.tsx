import {useEffect, useState} from "react";
import {api} from "../../../config/axios.ts";
import classes from "./styles/chats.module.css"
import Avatar from "react-avatar";
import ArrowIcon from "../../../assets/trip/arrow.svg";
import {Link, useNavigate} from "react-router-dom";


interface TestItem {
  id: string;
  title: string;
  description: string;
}

interface GetMyTestsResponse {
  testItems: TestItem[];
}

/**
 * Компонент с чатами пользователя в профиле
 */
function MyTests() {
  const [tests, setTests] = useState<TestItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api
      .get<GetMyTestsResponse>("test/GetMyTests")
      .then((response) => {
        setTests(response.data.testItems);
        setLoading(false);
      })
      .catch((error) => {
        setError("Error fetching my tests");
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div className={classes.chatsWrapper}>
      <h1>Ваши созданные тесты</h1>
      <ul>
        {tests.map((test, index) => (
          <li key={index} className={classes.testItemCard}>
            <h2>
              <Link to={`/editTest/${test.id}`}>{test.title}</Link>
            </h2>
            <p>{test.description}</p>
            <div className={classes.linksWrapper}>
              <Link to={`/userResults/${test.id}`} className={classes.userResultsLink}>
                Перейти к результатам пользователей
              </Link>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default MyTests;