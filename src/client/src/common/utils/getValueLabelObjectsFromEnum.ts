export interface ValueLabelObject<T> {
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
 * const objects = getValueLabelObjectsFromEnum<ConnectorDirection>(ConnectorDirection);
 *
 * // { value: 0, label: "Input" }
 * // { value: 1, label: "Output" }
 * // { value: 2, label: "Bidirectional" }
 *
 * @param enumObject
 */
export const getValueLabelObjectsFromEnum = <T>(enumObject: EnumObject): ValueLabelObject<T>[] => {
  return Object.keys(enumObject)
    .filter((v) => isNaN(Number(v)))
    .map((k) => ({ value: enumObject[k] as unknown as T, label: k }));
};
