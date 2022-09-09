import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useGetInterface } from "../../../data/queries/tyle/queriesInterface";
import { FormInterfaceLib, mapInterfaceLibCmToFormInterfaceLib } from "./types/formInterfaceLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes interface data as parameter and populates form
 */
export const usePrefilledInterfaceData = (
  reset: (values?: DefaultValues<FormInterfaceLib> | FormInterfaceLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const interfaceQuery = useGetInterface(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && interfaceQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapInterfaceLibCmToFormInterfaceLib(interfaceQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, interfaceQuery.isSuccess, interfaceQuery.data, reset]);

  return [hasPrefilled, interfaceQuery.isLoading];
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributeIdList");
};
