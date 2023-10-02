import { DevTool } from "@hookform/devtools";
import { yupResolver } from "@hookform/resolvers/yup";
import { State, TerminalLibCm } from "@mimirorg/typelibrary-types";
import { useServerValidation } from "common/hooks/server-validation/useServerValidation";
import { useNavigateOnCriteria } from "common/hooks/useNavigateOnCriteria";
import { Box, FormContainer } from "@mimirorg/component-library";
import { Loader } from "features/common/loader";
import { FormAttributes } from "features/entities/common/form-attributes/FormAttributes";
import { onSubmitForm } from "features/entities/common/utils/onSubmitForm";
import { prepareAttributes } from "features/entities/common/utils/prepareAttributes";
import { usePrefilledForm } from "features/entities/common/utils/usePrefilledForm";
import { useSubmissionToast } from "features/entities/common/utils/useSubmissionToast";
import { useTerminalMutation, useTerminalQuery } from "features/entities/terminal/TerminalForm.helpers";
import { TerminalFormBaseFields } from "features/entities/terminal/TerminalFormBaseFields";
import { terminalSchema } from "features/entities/terminal/terminalSchema";
import {
  createEmptyFormTerminalLib,
  FormTerminalLib,
  mapFormTerminalLibToApiModel,
  mapTerminalLibCmToFormTerminalLib,
} from "features/entities/terminal/types/formTerminalLib";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../types/formMode";
import { FormAttributeGroups } from "../common/form-attributeGroup/FormAttributeGroups";

interface TerminalFormProps {
  defaultValues?: FormTerminalLib;
  mode?: FormMode;
}

export const TerminalForm = ({ defaultValues = createEmptyFormTerminalLib(), mode }: TerminalFormProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");

  const formMethods = useForm<FormTerminalLib>({
    defaultValues: defaultValues,
    resolver: yupResolver(terminalSchema(t)),
  });

  const { register, handleSubmit, control, setError, reset } = formMethods;

  const attributeFields = useFieldArray({ control, name: "attributes" });
  const attributeGroupFields = useFieldArray({ control, name: "attributeGroups" });

  const query = useTerminalQuery();
  const mapper = (source: TerminalLibCm) => mapTerminalLibCmToFormTerminalLib(source);
  const [_, isLoading] = usePrefilledForm(query, mapper, reset);

  const mutation = useTerminalMutation(query.data?.id, mode);
  useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast(t("terminal.title"));

  const limited = mode === "edit" && query.data?.state === State.Approved;

  return (
    <FormProvider {...formMethods}>
      <FormContainer
        onSubmit={handleSubmit((data) => onSubmitForm(mapFormTerminalLibToApiModel(data), mutation.mutateAsync, toast))}
      >
        {isLoading && <Loader />}
        {!isLoading && (
          <>
            <TerminalFormBaseFields limited={limited} mode={mode} />

            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormAttributes
                register={(index) => register(`attributes.${index}`)}
                fields={attributeFields.fields}
                append={attributeFields.append}
                remove={attributeFields.remove}
                preprocess={prepareAttributes}
                canAddAttributes={!limited}
                canRemoveAttributes={!limited}
              />
            </Box>
            <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.mimirorg.spacing.multiple(6)}>
              <FormAttributeGroups
                register={(index) => register(`attributeGroups.${index}`)}
                fields={attributeGroupFields.fields}
                append={attributeGroupFields.append}
                remove={attributeGroupFields.remove}
                canAddAttributeGroups={!limited}
                canRemoveAttributeGroups={!limited}
              />
            </Box>
          </>
        )}
        <DevTool control={control} placement={"bottom-right"} />
      </FormContainer>
    </FormProvider>
  );
};
