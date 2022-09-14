import { useParams } from "react-router-dom";
import { useCreateTransport, useGetTransport, useUpdateTransport } from "../../../data/queries/tyle/queriesTransport";
import { FormTransportLib } from "./types/formTransportLib";

export const useTransportQuery = () => {
  const { id } = useParams();
  return useGetTransport(id);
};

export const useTransportMutation = (isEdit?: boolean) => {
  const createMutation = useCreateTransport();
  const updateMutation = useUpdateTransport();
  return isEdit ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormTransportLib) => void) => {
  resetField("attributeIdList");
};
