import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { useInterfaceMutation, useInterfaceQuery } from "features/entities/interface/InterfaceForm.helpers";
import { InterfaceFormContainer } from "features/entities/interface/InterfaceForm.styled";
import { InterfaceFormBaseFields } from "features/entities/interface/InterfaceFormBaseFields";
import { interfaceSchema } from "features/entities/interface/interfaceSchema";
import {
  createEmptyFormInterfaceLib,
  FormInterfaceLib,
  mapFormInterfaceLibToApiModel,
  mapInterfaceLibCmToFormInterfaceLib,
} from "features/entities/interface/types/formInterfaceLib";
import { InterfaceFormMode } from "features/entities/interface/types/interfaceFormMode";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

interface InterfaceFormProps {
  defaultValues?: FormInterfaceLib;
  mode?: InterfaceFormMode;
}

export const InterfaceForm = ({ defaultValues = createEmptyFormInterfaceLib(), mode }: InterfaceFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation();

  const formMethods = useForm<FormInterfaceLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(interfaceSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useInterfaceQuery();
  const mapper = (source: InterfaceLibCm) => mapInterfaceLibCmToFormInterfaceLib(source, mode);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useInterfaceMutation(mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("interface.title"));

  return (
    <FormProvider {...formMethods}>
      <InterfaceFormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(mapFormInterfaceLibToApiModel(data), mutation.mutateAsync, toast)
        )}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <InterfaceFormBaseFields mode={mode} />

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
      </InterfaceFormContainer>
    </FormProvider>
  );
};
