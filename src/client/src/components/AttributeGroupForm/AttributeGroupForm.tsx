import { DevTool } from "@hookform/devtools";
//import { yupResolver } from "@hookform/resolvers/yup";
import { useServerValidation } from "hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
//import { Loader } from "features/common/loader";
//import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
//import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
//import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { FormProvider, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../../common/types/formMode";
import {
  FormAttributeGroupLib,
  createEmptyFormAttributeGroupLib,
  mapFormAttributeGroupLibToApiModel,
} from "./formAttributeGroupLib";
//import { attributeGroupSchema } from "./attributeGroupSchema";
import { useAttributeGroupMutation, useAttributeGroupQuery } from "./AttributeGroupForm.helpers";
import { AttributeGroupFormBaseFields } from "./AttributeGroupFormBaseFields";

interface AttributeGroupFormProps {
  defaultValues?: FormAttributeGroupLib;
  mode?: FormMode;
}

const AttributeGroupForm = ({
  defaultValues = createEmptyFormAttributeGroupLib(),
  mode,
}: AttributeGroupFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

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

  const toast = useSubmissionToast(t("attributeGroup.title"));

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