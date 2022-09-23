import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { InterfaceLibCm } from "@mimirorg/typelibrary-types";
import { FormProvider, useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { useServerValidation } from "../../../hooks/useServerValidation";
import { Loader } from "../../common/loader";
import { FormAttributes } from "../common/form-attributes/FormAttributes";
import { onSubmitForm } from "../common/utils/onSubmitForm";
import { prepareAttributesByAspect } from "../common/utils/prepareAttributesByAspect";
import { usePrefilledForm } from "../common/utils/usePrefilledForm";
import { useSubmissionToast } from "../common/utils/useSubmissionToast";
import { useInterfaceMutation, useInterfaceQuery } from "./InterfaceForm.helpers";
import { InterfaceFormContainer } from "./InterfaceForm.styled";
import { InterfaceFormBaseFields } from "./InterfaceFormBaseFields";
import { interfaceSchema } from "./interfaceSchema";
import {
  createEmptyFormInterfaceLib,
  FormInterfaceLib,
  mapFormInterfaceLibToApiModel,
  mapInterfaceLibCmToFormInterfaceLib,
} from "./types/formInterfaceLib";
import { InterfaceFormMode } from "./types/interfaceFormMode";

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

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const query = useInterfaceQuery();
  const mapper = (source: InterfaceLibCm) => mapInterfaceLibCmToFormInterfaceLib(source, mode);
  const [isPrefilled, isLoading] = usePrefilledForm(query, mapper, reset);

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
            <InterfaceFormBaseFields isPrefilled={isPrefilled} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
              <FormAttributes
                register={(index) => register(`attributeIdList.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={(attributes) => prepareAttributesByAspect(attributes, [aspect])}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </InterfaceFormContainer>
    </FormProvider>
  );
};
