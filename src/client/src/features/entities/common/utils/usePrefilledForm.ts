import { useEffect, useState } from "react";
import { DefaultValues, KeepStateOptions } from "react-hook-form";
import { UseQueryResult } from "react-query";

/**
 * Hook ties together data from react query and react hook form binding
 *
 * @param fromDataQuery query which returns data to fill the form
 * @param mapDataToFormModel function which takes det response type of the data query and maps it to the form type
 * @param populateForm function which takes terminal data as parameter and populates the form
 */
export const usePrefilledForm = <TIn, TOut>(
  fromDataQuery: UseQueryResult<TIn>,
  mapDataToFormModel: (data: TIn) => TOut,
  populateForm: (values?: DefaultValues<TOut> | TOut, keepStateOptions?: KeepStateOptions) => void
): [isPrefilled: boolean, isLoading: boolean] => {
  const [isPrefilled, setIsPrefilled] = useState(false);

  useEffect(() => {
    if (!isPrefilled && fromDataQuery.isSuccess) {
      setIsPrefilled(true);
      populateForm(mapDataToFormModel(fromDataQuery.data), { keepDefaultValues: false });
    }
  }, [fromDataQuery.data, fromDataQuery.isSuccess, populateForm, isPrefilled, mapDataToFormModel]);

  return [isPrefilled, fromDataQuery.isLoading];
};
