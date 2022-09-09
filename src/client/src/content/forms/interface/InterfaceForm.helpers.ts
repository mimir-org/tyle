import { useParams } from "react-router-dom";
import { useCreateInterface, useGetInterface, useUpdateInterface } from "../../../data/queries/tyle/queriesInterface";
import { FormInterfaceLib } from "./types/formInterfaceLib";

export const useInterfaceQuery = () => {
  const { id } = useParams();
  return useGetInterface(id);
};

export const useInterfaceMutation = (isEdit?: boolean) => {
  const createMutation = useCreateInterface();
  const updateMutation = useUpdateInterface();
  return isEdit ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributeIdList");
};
