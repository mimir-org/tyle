import { useCreateTransport, useGetTransport, useUpdateTransport } from "external/sources/transport/transport.queries";
import { FormTransportLib } from "features/entities/transport/types/formTransportLib";
import { TransportFormMode } from "features/entities/transport/types/transportFormMode";
import { useParams } from "react-router-dom";

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
  resetField("attributes");
};
