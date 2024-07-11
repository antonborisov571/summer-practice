import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { api } from "../../config/axios.ts";
import { BeatLoader } from "react-spinners";
import Button from "../../components/general/button/button.tsx";
import classes from "./styles/combTests.module.css";

export type TestItem = {
  id: string;
  title: string;
  description: string;
};

export type GetMyTestsResponse = {
  testItems: TestItem[];
};

function CombTests() {
  const { id } = useParams<{ id: string }>();
  const [tests, setTests] = useState<TestItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [addedTests, setAddedTests] = useState<Set<string>>(new Set());

  useEffect(() => {
    api
      .get<GetMyTestsResponse>("test/GetMyTests")
      .then((response) => {
        setTests(response.data.testItems);
        setLoading(false);
      })
      .catch((error) => {
        console.log(error);
        setLoading(false);
      });
  }, []);

  const handleAddQuestions = (testId: string) => {
    api
      .post(`test/${id}/AddQuestionsFromTest`, { sourceTestId: testId })
      .then(() => {
        setAddedTests((prevSet) => new Set(prevSet).add(testId));
      })
      .catch((error) => {
        console.log(error);
      });
  };

  if (loading) return <BeatLoader />;

  return (
    <div className={classes.combTestsWrapper}>
      <h1>Выберите тесты для добавления вопросов</h1>
      <div className={classes.testsContainer}>
        {tests.map((test) => (
          <div key={test.id} className={classes.testCard}>
            <h3>{test.title}</h3>
            <p>{test.description}</p>
            <Button
              onClick={() => handleAddQuestions(test.id)}
              disabled={addedTests.has(test.id)}
            >
              {addedTests.has(test.id) ? "Вопросы добавлены" : "Добавить вопросы"}
            </Button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default CombTests;