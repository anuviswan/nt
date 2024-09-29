export interface Movie {
  title: string;
  language: string;
  releaseDate: Date;
  cast: Person[];
  crew: KeyValuePair<string,Person[]>[];
}


export interface KeyValuePair<TKey,TValue> {
  key: TKey;
  value: TValue;
}

export interface Person {
  name: string;
}
