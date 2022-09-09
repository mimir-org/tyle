import { DevTool } from "@hookform/devtools";
import { useFieldArray, useForm, useWatch } from "react-hook-form";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { useCreateInterface, useUpdateInterface } from "../../../data/queries/tyle/queriesInterface";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { Loader } from "../../common/Loader";
import { FormAttributes } from "../common/FormAttributes";
import { prepareAttributesByAspect } from "../common/prepareAttributesByAspect";
import { useInterfaceSubmissionToast, usePrefilledInterfaceData } from "./InterfaceForm.helpers";
import { InterfaceFormContainer } from "./InterfaceForm.styled";
import { InterfaceFormBaseFields } from "./InterfaceFormBaseFields";
import { createEmptyFormInterfaceLib, FormInterfaceLib, mapFormInterfaceLibToApiModel } from "./types/formInterfaceLib";

interface InterfaceFormProps {
  defaultValues?: FormInterfaceLib;
  isEdit?: boolean;
}

export const InterfaceForm = ({ defaultValues = createEmptyFormInterfaceLib(), isEdit }: InterfaceFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormInterfaceLib>({ defaultValues });

  const aspect = useWatch({ control, name: "aspect" });
  const attributeFields = useFieldArray({ control, name: "attributeIdList" });

  const [hasPrefilledData, isLoading] = usePrefilledInterfaceData(reset);

  const createMutation = useCreateInterface();
  const updateMutation = useUpdateInterface();
  const targetMutation = isEdit ? updateMutation : createMutation;

  const toastNodeSubmission = useInterfaceSubmissionToast();
  const onSubmit = (data: FormInterfaceLib) => {
    const submittable = mapFormInterfaceLibToApiModel(data);
    const submissionPromise = targetMutation.mutateAsync(submittable);
    toastNodeSubmission(submissionPromise);
    return submissionPromise;
  };

  useNavigateOnCriteria("/", targetMutation.isSuccess);

  return (
    <InterfaceFormContainer onSubmit={handleSubmit((data) => onSubmit(data))}>
      {isLoading && <Loader />}
      {!isLoading && (
        <>
          <InterfaceFormBaseFields
            control={control}
            register={register}
            resetField={resetField}
            setValue={setValue}
            hasPrefilledData={hasPrefilledData}
          />

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
  );
};
