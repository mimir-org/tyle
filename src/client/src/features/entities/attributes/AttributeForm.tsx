import { DevTool } from "@hookform/devtools";
import { AttributeLibAm, AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  createEmptyFormAttributeLib,
  mapAttributeLibCmToFormAttributeLib,
  mapFormAttributeLibToApiModel,
} from "./types/formAttributeLib";
import { useAttributeMutation, useAttributeQuery } from "./AttributeForm.helpers";
import { AttributeFormContainer } from "./AttributeFormContainer.styled";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";

interface AttributeFormProps {
  defaultValues?: AttributeLibAm;
}

export const AttributeForm = ({ defaultValues = createEmptyFormAttributeLib() }: AttributeFormProps) => {
  const { t } = useTranslation("entities");

  const formMethods = useForm<AttributeLibAm>({
    defaultValues: defaultValues,
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useAttributeQuery();
  const mapper = (source: AttributeLibCm) => mapAttributeLibCmToFormAttributeLib(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeMutation(query.data?.id);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);
  console.log(mutation);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      {isLoading ? (
        <Loader />
      ) : (
        <>
          <AttributeFormBaseFields />
          <AttributeFormContainer
            onSubmit={handleSubmit((data) =>
              // console.log("this is the data from the form", data),
              onSubmitForm(mapFormAttributeLibToApiModel(data), mutation.mutateAsync, toast)
            )}
          >
            <DevTool control={control} placement={"bottom-right"} />
          </AttributeFormContainer>
        </>
      )}
    </FormProvider>
  );
};
