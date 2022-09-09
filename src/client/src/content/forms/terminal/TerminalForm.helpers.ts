import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useGetTerminal } from "../../../data/queries/tyle/queriesTerminal";
import { FormTerminalLib, mapTerminalLibCmToFormTerminalLib } from "./types/formTerminalLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes terminal data as parameter and populates form
 */
export const usePrefilledTerminalData = (
  reset: (values?: DefaultValues<FormTerminalLib> | FormTerminalLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const terminalQuery = useGetTerminal(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && terminalQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapTerminalLibCmToFormTerminalLib(terminalQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, terminalQuery.isSuccess, terminalQuery.data, reset]);

  return [hasPrefilled, terminalQuery.isLoading];
};

export const prepareAttributes = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return attributes.sort((a, b) => a.discipline - b.discipline);
};
