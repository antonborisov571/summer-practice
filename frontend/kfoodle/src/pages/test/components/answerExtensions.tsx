import React from 'react';
import Radio from "../../../components/general/radio/radio.tsx";
import Checkbox from "../../../components/general/checkbox/checkbox.tsx";
import InputForm from "../../../components/general/inputform/inputform.tsx";
import {api} from "../../../config/axios.ts";

const SingleAnswerTaskExtension = ({ id, task, onChange, testAttemptId }) => {
  const handleInputChange = (index) => {
    const updatedChoices = task.choices.map((choice, i) => ({
      ...choice,
      isSelected: i === index
    }));
    onChange(task.id, updatedChoices);

    api
      .post('answer/singleAnswer', {
        testAttemptId,
        questionId: task.id,
        choiceId: updatedChoices.find(x => x.isSelected).id,
      })
      .then()
      .catch()
  };

  return (
    <div>
      {task.choices.map((choice, index) => (
        <div key={index} style={{display:"flex", alignItems: "center"}}>
          <Radio
            id={`${id}-${index}`}
            name={id}
            value={choice.choiceText}
            checked={choice.isSelected}
            onChange={() => handleInputChange(index)}
          />
          <label htmlFor={`${id}-${index}`}>{choice.choiceText}</label>
        </div>
      ))}
    </div>
  );
};

const MultipleAnswersTaskExtension = ({ id, task, onChange, testAttemptId }) => {
  const handleInputChange = (index) => {
    const updatedChoices = task.choices.map((choice, i) => ({
      ...choice,
      isSelected: i === index ? !choice.isSelected : choice.isSelected
    }));
    onChange(task.id, updatedChoices);


    api
      .post('answer/multipleAnswer', {
        testAttemptId,
        questionId: task.id,
        choicesId: updatedChoices.filter(x => x.isSelected).map(x => x.id),
      })
      .then()
      .catch()
  };

  return (
    <div>
      {task.choices.map((choice, index) => (
        <div key={index} style={{display:"flex", alignItems: "center"}}>
          <Checkbox
            id={`${id}-${index}`}
            type="checkbox"
            name={id}
            value={choice.choiceText}
            checked={choice.isSelected}
            onChange={() => handleInputChange(index)}
          />
          <label htmlFor={`${id}-${index}`}>{choice.choiceText}</label>
        </div>
      ))}
    </div>
  );
};

const InputAnswersTaskExtension = ({ id, task, onChange, testAttemptId }) => {
  const handleInputChange = (event) => {
    onChange(task.id, event.target.value);

    api
      .post('answer/InputAnswer', {
        testAttemptId,
        questionId: task.id,
        answer: event.target.value,
      })
      .then()
      .catch()
  };

  return (
    <div>
      <InputForm
        id={id}
        value={task.answer || ''}
        onChange={handleInputChange}
      />
    </div>
  );
};

export { SingleAnswerTaskExtension, MultipleAnswersTaskExtension, InputAnswersTaskExtension };