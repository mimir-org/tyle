import { useParams } from "react-router-dom";
import { useGetTransport } from "../../../data/queries/tyle/queriesTransport";
import { FormTransportLib } from "./types/formTransportLib";

export const useTransportQuery = () => {
  const { id } = useParams();
  return useGetTransport(id);
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormTransportLib) => void) => {
  resetField("attributeIdList");
};
