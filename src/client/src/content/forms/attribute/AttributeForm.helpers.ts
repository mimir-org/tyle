import { Select } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { useParams } from "react-router-dom";
import { useGetAttribute } from "../../../data/queries/tyle/queriesAttribute";
import { FormAttributeLib, mapAttributeLibCmToFormAttributeLib } from "./types/formAttributeLib";

/**
 * Hook ties together params from react router, node data from react query and react hook form binding
 *
 * @param reset function which takes node data as parameter and populates form
 */
export const usePrefilledAttributeData = (
  reset: (values?: DefaultValues<FormAttributeLib> | FormAttributeLib, keepStateOptions?: KeepStateOptions) => void
): [hasPrefilled: boolean, isLoading: boolean] => {
  const { id } = useParams();
  const attributeQuery = useGetAttribute(id);
  const [hasPrefilled, setHasPrefilled] = useState(false);

  useEffect(() => {
    if (!hasPrefilled && attributeQuery.isSuccess) {
      setHasPrefilled(true);
      reset(mapAttributeLibCmToFormAttributeLib(attributeQuery.data), { keepDefaultValues: false });
    }
  }, [hasPrefilled, attributeQuery.isSuccess, attributeQuery.data, reset]);

  return [hasPrefilled, attributeQuery.isLoading];
};

export const showSelectValues = (attributeSelect: Select) => attributeSelect !== Select.None;
