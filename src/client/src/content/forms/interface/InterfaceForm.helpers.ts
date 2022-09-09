import { useParams } from "react-router-dom";
import { useGetInterface } from "../../../data/queries/tyle/queriesInterface";
import { FormInterfaceLib } from "./types/formInterfaceLib";

export const useInterfaceQuery = () => {
  const { id } = useParams();
  return useGetInterface(id);
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributeIdList");
};
