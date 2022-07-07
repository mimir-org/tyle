import { DevTool } from "@hookform/devtools";
import { Aspect } from "@mimirorg/typelibrary-types";
import { Controller, useForm, useWatch } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../assets/text/TextResources";
import { Button } from "../../../complib/buttons";
import { Popover } from "../../../complib/data-display";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Box, Flexbox } from "../../../complib/layouts";
import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";
import { ConditionalWrapper } from "../../../complib/utils";
import { useCreateNode, useUpdateNode } from "../../../data/queries/tyle/queriesNode";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetSymbols } from "../../../data/queries/tyle/queriesSymbol";
import { useNavigateOnCriteria } from "../../../hooks/useNavigateOnCriteria";
import { PlainLink } from "../../utils/PlainLink";
import { createEmptyFormNodeLib, FormNodeLib } from "../types/formNodeLib";
import { aspectOptions, resetSubform, submitNodeData, usePrefilledNodeData } from "./NodeForm.helpers";
import { NodeFormPreview } from "./NodeFormPreview";
import { FunctionNode, LocationNode, ProductNode } from "./variants";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
  isEdit?: boolean;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLib(), isEdit }: NodeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset, resetField } = useForm<FormNodeLib>({ defaultValues });

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();

  const aspect = useWatch({ control, name: "aspect" });

  const hasPrefilledData = usePrefilledNodeData(reset);

  const nodeUpdateMutation = useUpdateNode();
  const nodeCreateMutation = useCreateNode();
  useNavigateOnCriteria("/", nodeCreateMutation.isSuccess || nodeUpdateMutation.isSuccess);

  return (
    <Box
      as={"form"}
      flex={1}
      display={"flex"}
      flexWrap={"wrap"}
      justifyContent={"center"}
      gap={`min(${theme.tyle.spacing.multiple(14)}, 8vw)`}
      px={`min(${theme.tyle.spacing.multiple(11)}, 5vw)`}
      py={theme.tyle.spacing.multiple(6)}
      onSubmit={handleSubmit((data) =>
        submitNodeData(data, isEdit ? nodeUpdateMutation.mutateAsync : nodeCreateMutation.mutateAsync)
      )}
    >
      <Box
        as={"fieldset"}
        flex={1}
        display={"flex"}
        flexDirection={"column"}
        flexGrow={"0"}
        alignItems={"center"}
        gap={theme.tyle.spacing.xl}
        border={0}
        p={"0"}
      >
        <NodeFormPreview control={control} />

        <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
          <FormField label={textResources.FORMS_NODE_NAME}>
            <Input placeholder={textResources.FORMS_NODE_NAME_PLACEHOLDER} {...register("name")} />
          </FormField>
          <FormField label={textResources.FORMS_NODE_PURPOSE}>
            <Controller
              control={control}
              name={"purposeName"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.FORMS_NODE_PURPOSE_PLACEHOLDER}
                  options={purposeQuery.data}
                  isLoading={purposeQuery.isLoading}
                  getOptionLabel={(x) => x.name}
                  getOptionValue={(x) => x.name}
                  onChange={(x) => onChange(x?.name)}
                  value={purposeQuery.data?.find((x) => x.name === value)}
                />
              )}
            />
          </FormField>
          <FormField label={textResources.FORMS_NODE_ASPECT}>
            <Controller
              control={control}
              name={"aspect"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <ConditionalWrapper
                  condition={hasPrefilledData}
                  wrapper={(c) => (
                    <Popover align={"start"} maxWidth={"225px"} content={textResources.FORMS_NODE_ASPECT_DISABLED}>
                      <Box borderRadius={theme.tyle.border.radius.medium} tabIndex={0}>
                        {c}
                      </Box>
                    </Popover>
                  )}
                >
                  <Select
                    {...rest}
                    selectRef={ref}
                    placeholder={textResources.FORMS_NODE_ASPECT_PLACEHOLDER}
                    options={aspectOptions}
                    getOptionLabel={(x) => x.label}
                    onChange={(x) => {
                      resetSubform(resetField);
                      onChange(x?.value);
                    }}
                    value={aspectOptions.find((x) => x.value === value)}
                    isDisabled={hasPrefilledData}
                  />
                </ConditionalWrapper>
              )}
            />
          </FormField>
          <FormField label={textResources.FORMS_NODE_SYMBOL}>
            <Controller
              control={control}
              name={"symbol"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.FORMS_NODE_SYMBOL_PLACEHOLDER}
                  options={symbolQuery.data}
                  isLoading={symbolQuery.isLoading}
                  getOptionLabel={(x) => x.name}
                  getOptionValue={(x) => x.data}
                  onChange={(x) => onChange(x?.data)}
                  value={symbolQuery.data?.find((x) => x.data === value)}
                  formatOptionLabel={(x) => (
                    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.base}>
                      <Icon src={x.data} />
                      <Text>{x.name}</Text>
                    </Flexbox>
                  )}
                />
              )}
            />
          </FormField>
          <Input type={"hidden"} {...register("rdsCode")} />
          <FormField label={textResources.FORMS_NODE_RDS}>
            <Controller
              control={control}
              name={"rdsName"}
              render={({ field: { value, onChange, ref, ...rest } }) => (
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.FORMS_NODE_RDS_PLACEHOLDER}
                  options={rdsQuery.data}
                  isLoading={rdsQuery.isLoading}
                  getOptionLabel={(x) => `${x.id} - ${x.name}`}
                  getOptionValue={(x) => x.id}
                  value={rdsQuery.data?.find((x) => x.name === value)}
                  onChange={(rds) => {
                    if (rds) {
                      setValue("rdsCode", rds.id, { shouldDirty: true });
                      onChange(rds.name);
                    }
                  }}
                />
              )}
            />
          </FormField>
          <FormField label={textResources.FORMS_NODE_DESCRIPTION}>
            <Textarea placeholder={textResources.FORMS_NODE_DESCRIPTION_PLACEHOLDER} {...register("description")} />
          </FormField>
        </Flexbox>

        <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
          <PlainLink tabIndex={-1} to={"/"}>
            <Button tabIndex={0} as={"span"} variant={"outlined"}>
              {textResources.FORMS_CANCEL}
            </Button>
          </PlainLink>
          <Button type={"submit"}>{textResources.FORMS_SUBMIT}</Button>
        </Flexbox>
      </Box>

      <Box display={"flex"} flex={3} flexDirection={"column"} gap={theme.tyle.spacing.multiple(6)}>
        {aspect === Aspect.Function && <FunctionNode control={control} register={register} />}
        {aspect === Aspect.Location && <LocationNode control={control} register={register} />}
        {aspect === Aspect.Product && <ProductNode control={control} register={register} />}
      </Box>

      <DevTool control={control} placement={"bottom-right"} />
    </Box>
  );
};
