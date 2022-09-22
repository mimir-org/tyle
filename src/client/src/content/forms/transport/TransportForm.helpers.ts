import { useParams } from "react-router-dom";
import { useCreateTransport, useGetTransport, useUpdateTransport } from "../../../data/queries/tyle/queriesTransport";
import { FormTransportLib } from "./types/formTransportLib";
import { TransportFormMode } from "./types/transportFormMode";

export const useTransportQuery = () => {
  const { id } = useParams();
  return useGetTransport(id);
};

export const useTransportMutation = (mode?: TransportFormMode) => {
  const createMutation = useCreateTransport();
  const updateMutation = useUpdateTransport();
  return mode === "edit" ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormTransportLib) => void) => {
  resetField("attributeIdList");
};
