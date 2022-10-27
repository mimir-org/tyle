import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Loader } from "../../../common/components/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributes } from "../common/utils/prepareAttributes";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { useTransportMutation, useTransportQuery } from "./TransportForm.helpers";
import { TransportFormContainer } from "./TransportForm.styled";
import { TransportFormBaseFields } from "./TransportFormBaseFields";
import { transportSchema } from "./transportSchema";
import {
  createEmptyFormTransportLib,
  FormTransportLib,
  mapFormTransportLibToApiModel,
  mapTransportLibCmToFormTransportLib,
} from "./types/formTransportLib";
import { TransportFormMode } from "./types/transportFormMode";

interface TransportFormProps {
  defaultValues?: FormTransportLib;
  mode?: TransportFormMode;
}

export const TransportForm = ({ defaultValues = createEmptyFormTransportLib(), mode }: TransportFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const formMethods = useForm<FormTransportLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(transportSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useTransportQuery();
  const mapper = (source: TransportLibCm) => mapTransportLibCmToFormTransportLib(source, mode);
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useTransportMutation(mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("transport.title"));

  return (
    <FormProvider {...formMethods}>
      <TransportFormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(mapFormTransportLibToApiModel(data), mutation.mutateAsync, toast)
        )}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <TransportFormBaseFields isPrefilled={isPrefilled} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </TransportFormContainer>
    </FormProvider>
  );
};
