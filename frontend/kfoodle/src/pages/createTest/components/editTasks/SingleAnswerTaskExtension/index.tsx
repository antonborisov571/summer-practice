import classes from "./index.module.css";
import { SingleAnswerQuestion } from "../../../../../hooks/types/Question.ts";
import Radio from "../../../../../components/general/radio/radio";
import InputForm from "../../../../../components/general/inputform/inputform";
import Button from "../../../../../components/general/button/button";
import TrashButton from "../../../../../components/general/trashButton";

function SingleAnswerTaskExtension({
  id,
  task,
  onChange,
}: {
  id: string;
  task: SingleAnswerQuestion;
  onChange: (attr: string, value: any) => void;
}) {
  const setRightVariant = (event: any) => {
    const currentVariantId = Number(event.target.value);
    const updatedVariants = task.choices.map((variant) => {
      if (variant.id === currentVariantId) {
        return { ...variant, isCorrect: true };
      }
      return { ...variant, isCorrect: false };
    });
    onChange("choices", updatedVariants);
  };

  const updateVariantInput = (id: number, description: string) => {
    const newVariants = task.choices;
    newVariants!.find((v) => v.id == id)!.choiceText = description;
    onChange("choices", newVariants);
  };

  const addVariant = (event: any) => {
    event.preventDefault();
    const newId =
      task.choices.length == 0
        ? 1
        : task.choices[task.choices.length - 1].id + 1;
    onChange(
      "choices",
      task.choices!.concat({
        id: newId,
        choiceText: "",
        isCorrect: false,
      })
    );
  };

  const removeVariant = (variantId: number) => {
    const newTaskVariants = task.choices.filter((v) => v.id != variantId);
    onChange("choices", newTaskVariants);
    if (newTaskVariants.every((v) => !v.isCorrect))
      onChange(
        "choices",
        newTaskVariants.map((v, i) => {
          if (i == 0) {
            return { ...v, isCorrect: true };
          }
          return v;
        })
      );
  };

  return (
    <div id={id} className={classes.variantsContainer}>
      {task.choices?.map((variant) => {
        return (
          <div key={variant.id} className={classes.variant}>
            <Radio
              name={task.id}
              id={variant.id}
              value={variant.id}
              onChange={setRightVariant}
              checked={variant.isCorrect ? "t" : ""}
            ></Radio>
            <InputForm
              style={{ width: "100%" }}
              value={variant.choiceText}
              onChange={(event: any) =>
                updateVariantInput(variant.id, event.target.value)
              }
            />
            {task.choices.length > 2 && (
              <TrashButton
                onClick={(event: any) => {
                  event.preventDefault();
                  removeVariant(variant.id);
                }}
              />
            )}
          </div>
        );
      })}
      <Button onClick={addVariant} style={{ width: "fit-content" }}>
        добавить пункт
      </Button>
    </div>
  );
}

export default SingleAnswerTaskExtension;
