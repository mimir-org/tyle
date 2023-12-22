import { DevTool } from "@hookform/devtools";
import { Box, FormContainer } from "@mimirorg/component-library";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { useServerValidation } from "hooks/useServerValidation";
import { FormProvider, useForm } from "react-hook-form";
import { useTheme } from "styled-components";
import { FormMode } from "types/formMode";
import { useAttributeGroupMutation, useAttributeGroupQuery } from "./AttributeGroupForm.helpers";
import AttributeGroupFormBaseFields from "./AttributeGroupFormBaseFields";
import {
  FormAttributeGroupLib,
  createEmptyFormAttributeGroupLib,
  mapFormAttributeGroupLibToApiModel,
} from "./formAttributeGroupLib";

interface AttributeGroupFormProps {
  defaultValues?: FormAttributeGroupLib;
  mode?: FormMode;
}

const AttributeGroupForm = ({ defaultValues = createEmptyFormAttributeGroupLib(), mode }: AttributeGroupFormProps) => {
  const theme = useTheme();

  const formMethods = useForm<FormAttributeGroupLib>({
    defaultValues: defaultValues,
    //resolver: yupResolver(attributeGroupSchema(t)),
  });

  const { handleSubmit, control, setError } = formMethods;

  //const attributeFields = useFieldArray({ control, name: "attributes" });

  const query = useAttributeGroupQuery();
  //const mapper = (source: AttributeGroupLibCm) => mapAttributeGroupLibCmToFormAttributeGroupLib(source);
  //const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useAttributeGroupMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute group");

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) =>
          onSubmitForm(mapFormAttributeGroupLibToApiModel(data), mutation.mutateAsync, toast),
        )}
      >
        {/*isLoading && <Loader />*/}
        {
          /*!isLoading && */ <>
            <AttributeGroupFormBaseFields mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              {/*<FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
            />*/}
            </Box>
          </>
        }
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};

export default AttributeGroupForm;
