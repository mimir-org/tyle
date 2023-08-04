import { UseQueryResult } from "@tanstack/react-query";
import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";

/**
 * Hook ties together data from react query and react hook form binding
 *
 * @param query returns data to fill the form
 * @param mapQueryDataToFormModel takes the response type of the data query and maps it to the form type
 * @param populateForm populates the form with the provided data
 */
export const usePrefilledForm = <TIn, TOut>(
  query: UseQueryResult<TIn>,
  mapQueryDataToFormModel: (data: TIn) => TOut,
  populateForm: (values?: DefaultValues<TOut> | TOut, keepStateOptions?: KeepStateOptions) => void,
): [isPrefilled: boolean, isLoading: boolean] => {
  const [isPrefilled, setIsPrefilled] = useState(false);

  useEffect(() => {
    if (!isPrefilled && query.isSuccess) {
      setIsPrefilled(true);
      populateForm(mapQueryDataToFormModel(query.data), { keepDefaultValues: false });
    }
  }, [query.data, query.isSuccess, populateForm, isPrefilled, mapQueryDataToFormModel]);

  return [isPrefilled, query.isInitialLoading];
};
