export enum QuestionType {
  SingleAnswer = 0,
  MultipleAnswers,
  InputAnswer,
  SequenceAnswer,
}
``;

export type Choice = {
  id: number;
  choiceText: string;
  isCorrect: boolean;
};

export type Question = {
  id: number;
  questionText: string;
  questionType: QuestionType;
  choices: Choice[];
  rightAnswer: string;
};

export type SingleAnswerQuestion = Question & {
  choices: Choice[];
};

export type MultipleAnswersQuestion = Question & {
  choices: Choice[];
};

export type InputAnswersQuestion = Question & {
  rightAnswer: string;
};
