import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box } from "complib/layouts";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormTerminalLib, mapFormTerminalLibToApiModel } from "features/entities/terminal/types/formTerminalLib";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import {
  createEmptyFormAttributeLib,
  FormAttributeLib,
  mapAttributeLibCmToFormAttributeLib,
} from "./types/formAttributeLib";
import { useAttributeMutation, useAttributeQuery } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeFormContainer.styled";
import { attributeSchema } from "./attributeSchema";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<FormTerminalLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(attributeSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;
  const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useAttributeQuery();
  const mapper = (source: AttributeLibCm) => mapAttributeLibCmToFormAttributeLib(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeMutation(query.data?.id);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      <AttributeFormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(mapFormTerminalLibToApiModel(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <AttributeFormBaseFields />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                canRemoveAttributes={false}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </AttributeFormContainer>
    </FormProvider>
  );
};
