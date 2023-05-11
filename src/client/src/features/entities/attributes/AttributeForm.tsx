import { DevTool } from "@hookform/devtools";
import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Loader } from "features/common/loader";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useAttributeMutation, useAttributeQuery } from "./AttributeForm.helpers";
import { AttributeFormBaseFields } from "./AttributeFormBaseFields";
import {
  createEmptyAttribute,
  FormAttributeLib,
  fromFormAttributeLibToApiModel,
  toFormAttributeLib,
} from "./types/formAttributeLib";
import { AttributeFormPreview } from "../entityPreviews/attribute/AttributeFormPreview";
import { FormContainer } from "../../../complib/form/FormContainer.styled";

interface AttributeFormProps {
  defaultValues?: FormAttributeLib;
}

export const AttributeForm = ({ defaultValues = createEmptyAttribute() }: AttributeFormProps) => {
  const { t } = useTranslation("entities");

  const formMethods = useForm<FormAttributeLib>({
    defaultValues: defaultValues,
  });

  const { handleSubmit, control, setError, reset } = formMethods;

  const query = useAttributeQuery();
  const mapper = (source: AttributeLibCm) => toFormAttributeLib(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeMutation(query.data?.id, true);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("attribute.title"));

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(fromFormAttributeLibToApiModel(data), mutation.mutateAsync, toast)
        )}
      >
        {isLoading ? (
          <Loader />
        ) : (
          <>
            <AttributeFormBaseFields />
            <AttributeFormPreview control={control} />
            <DevTool control={control} placement={"bottom-right"} />
          </>
        )}
      </FormContainer>
    </FormProvider>
  );
};
