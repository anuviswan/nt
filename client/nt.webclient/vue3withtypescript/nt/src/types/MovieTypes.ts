export interface Movie {
  title: string;
  language: string;
  releaseDate: Date;
  cast: Person[];
  crew: { [key: string]: Person };
}

export interface Person {
  name: string;
}
