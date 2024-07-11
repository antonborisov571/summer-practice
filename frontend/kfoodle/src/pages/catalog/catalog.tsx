import { useEffect, useState } from "react";
import { api } from "../../config/axios.ts";
import { Link } from "react-router-dom";
import classes from "./styles/catalog.module.css";

interface TestItem {
  id: string;
  title: string;
  description: string;
}

interface GetCatalogResponse {
  testItems: TestItem[];
}

function Catalog() {
  const [catalog, setCatalog] = useState<TestItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    api
      .get<GetCatalogResponse>("test/GetCatalog")
      .then((response) => {
        setCatalog(response.data.testItems);
        setLoading(false);
      })
      .catch((error) => {
        setError("Error fetching catalog");
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
    <div className={classes.catalogWrapper}>
      <h1>Каталог тестов</h1>
      <ul>
        {catalog.map((item, index) => (
          <li key={index} className={classes.testItemCard}>
            <h2>
              <Link to={`/test/${item.id}`}>{item.title}</Link>
            </h2>
            <p>{item.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Catalog;