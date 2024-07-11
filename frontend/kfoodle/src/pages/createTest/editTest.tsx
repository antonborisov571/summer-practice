import InputForm from "../../components/general/inputform/inputform.tsx";
import classes from "./styles/createTest.module.css"
import Button from "../../components/general/button/button.tsx";
import {useEffect, useState} from "react";
import { api } from "../../config/axios.ts"
import {Navigate, useNavigate, useParams} from "react-router-dom";
import {useProfile} from "../../hooks/profile/useProfile.ts";

function EditTest() {
  const { id } = useParams();
  const navigator = useNavigate();
  const [profile, loadingProfile] = useProfile();

  const [title, setTitle] = useState<string>("")
  const [description, setDescription] = useState<string>("")
  const [endDate, setEndDate] = useState<Date>();
  const [duration, setDuration] = useState<number>();
  const [numQuestions, setNumQuestions] = useState<number>();
  const [maxAttempts, setMaxAttempts] = useState<number>();


  useEffect(() => {
    api
      .get(`test/${id}/getEditTest`)
      .then(response =>{
        console.log(response);
        setTitle(response.data.title)
        setDescription(response.data.description)
        setEndDate(response.data.endDate)
        setDuration(response.data.duration)
        setNumQuestions(response.data.numQuestions)
        setMaxAttempts(response.data.maxAttempts)
      })
      .catch(error =>{
        navigator("/");
      })
  }, []);

  /**
   * Возвращает текущую дату и время в формате "yyyy-mm-ddT00:00".
   * @returns {string} Текущая дата и время в формате "yyyy-mm-ddT00:00".
   */
  const getCurrentDate = () => {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    let month: string | number = currentDate.getMonth() + 1;
    let day: string | number = currentDate.getDate();

    month = month < 10 ? '0' + month : month;
    day = day < 10 ? '0' + day : day;

    return `${year}-${month}-${day}T00:00`;
  }

  const saveTest = () => {
    api
      .patch("test/UpdateTest",
        {id, title, description, endDate, duration, numQuestions, maxAttempts})
      .catch()
  }

  const host = window.location.host;
  const protocol = window.location.protocol;

  if (profile == null && !loadingProfile) return <Navigate to="/login"></Navigate>;

  return (
   <div className={classes.createTestWrapper}>
     <h1>Редактирование теста</h1>
     <ul className={classes.inputsWrapper}>
       <div className={classes.inputWrapper}>
         <h3>Наименование</h3>
         <InputForm
           value={title}
           style={{minWidth: "700px"}}
           onChange={(e) => setTitle(e.target.value)}
         />
       </div>
       <div className={classes.inputWrapper}>
         <h3>Описание</h3>
         <textarea
           value={description}
           onChange={(e) => setDescription(e.target.value)}
           className={classes.input}
         ></textarea>
       </div>
       <div className={classes.inputTimesWrapper}>
         <h3>Время завершения</h3>
         <InputForm
           value={endDate}
           onChange={(e) => setEndDate(e.target.value)}
           type={"datetime-local"}
           min={getCurrentDate()}
         />
         <h3>Время на прохождение<br/> (в минутах)</h3>
         <InputForm
           value={duration}
           onChange={(e) => setDuration(e.target.value)}
           type={"number"}
           style={{minWidth: "245px"}}
         />
       </div>
       <div className={classes.inputWrapper}>
         <h3>Количество вопросов (если число меньше, чем всего вопросов,
           <br/>то будут выбраны рандомные вопросы)</h3>
         <InputForm
           value={numQuestions}
           onChange={(e) => setNumQuestions(e.target.value)}
           type={"number"}
           style={{minWidth: "245px"}}
         />
       </div>
       <div className={classes.inputWrapper}>
         <h3>Максимальное число попыток</h3>
         <InputForm
           value={maxAttempts}
           onChange={(e) => setMaxAttempts(e.target.value)}
           type={"number"}
           style={{minWidth: "245px"}}
         />
       </div>
       <div>
         <h3>Ссылка на тест: <a href={`${protocol}://${host}/test/${id}`}>{`${protocol}://${host}/test/${id}`}</a></h3>

       </div>
     </ul>
     <div className={classes.buttonsWrapper}>
       <Button onClick={saveTest}>Сохранить тест</Button>
       <Button onClick={() => navigator(`/combTests/${id}`)}>Комбинировать тесты</Button>
       <Button onClick={() => navigator(`/editQuestions/${id}`)}>Редактировать содержимое теста</Button>
     </div>
   </div>
  )
}

export default EditTest