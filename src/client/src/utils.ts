export interface Option<T> {
  value: T;
  label: string;
}

type EnumObject = { [key: string]: string | number };

/**
 * Helper which constructs value-label objects from non-const enums
 *
 * @example
 * enum ConnectorDirection
 * {
 * 	Input = 0,
 * 	Output = 1,
 * 	Bidirectional = 2,
 * }
 *
 * const objects = getOptionsFromEnum<ConnectorDirection>(ConnectorDirection);
 *
 * // { value: 0, label: "Input" }
 * // { value: 1, label: "Output" }
 * // { value: 2, label: "Bidirectional" }
 *
 * @param enumObject
 */
export const getOptionsFromEnum = <T>(enumObject: EnumObject): Option<T>[] => {
  return Object.keys(enumObject)
    .filter((v) => isNaN(Number(v)))
    .map((k) => ({ value: enumObject[k] as unknown as T, label: addSpacesToPascalCase(k) }));
};

export const addSpacesToPascalCase = (s: string): string => {
  const wordsArray = s.split(/(?=[A-Z])/);
  return wordsArray.join(" ");
};
