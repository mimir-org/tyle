import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { TransportLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { useTransportMutation, useTransportQuery } from "features/entities/transport/TransportForm.helpers";
import { TransportFormContainer } from "features/entities/transport/TransportForm.styled";
import { TransportFormBaseFields } from "features/entities/transport/TransportFormBaseFields";
import { transportSchema } from "features/entities/transport/transportSchema";
import {
  createEmptyFormTransportLib,
  FormTransportLib,
  mapFormTransportLibToApiModel,
  mapTransportLibCmToFormTransportLib,
} from "features/entities/transport/types/formTransportLib";
import { TransportFormMode } from "features/entities/transport/types/transportFormMode";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

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
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

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
            <TransportFormBaseFields mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                canRemoveAttributes={mode !== "edit"}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </TransportFormContainer>
    </FormProvider>
  );
};
