import { useCreateInterface, useGetInterface, useUpdateInterface } from "external/sources/interface/interface.queries";
import { FormInterfaceLib } from "features/entities/interface/types/formInterfaceLib";
import { InterfaceFormMode } from "features/entities/interface/types/interfaceFormMode";
import { useParams } from "react-router-dom";

export const useInterfaceQuery = () => {
  const { id } = useParams();
  return useGetInterface(id);
};

export const useInterfaceMutation = (mode?: InterfaceFormMode) => {
  const createMutation = useCreateInterface();
  const updateMutation = useUpdateInterface();
  return mode === "edit" ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributes");
};
