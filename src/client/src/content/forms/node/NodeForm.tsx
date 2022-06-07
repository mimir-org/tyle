import { DevTool } from "@hookform/devtools";
import { Controller, useForm, useWatch } from "react-hook-form";
import { useTheme } from "styled-components/macro";
import textResources from "../../../assets/text/TextResources";
import { Button } from "../../../complib/buttons";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Box, Flexbox } from "../../../complib/layouts";
import { Icon } from "../../../complib/media";
import { Text } from "../../../complib/text";
import { useCreateNode } from "../../../data/queries/tyle/queriesNode";
import { useGetPurposes } from "../../../data/queries/tyle/queriesPurpose";
import { useGetRds } from "../../../data/queries/tyle/queriesRds";
import { useGetSymbols } from "../../../data/queries/tyle/queriesSymbol";
import { useGetTerminals } from "../../../data/queries/tyle/queriesTerminal";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { getColorFromAspect } from "../../../utils/getColorFromAspect";
import { NodePreview } from "../../home/components/about/components/node/NodePreview";
import { createEmptyFormNodeLibAm, FormNodeLib, mapFormNodeLibAmToApiModel } from "../types/formNodeLib";
import { aspectOptions, getTerminalItemsFromFormData, usePrefilledNodeData } from "./NodeForm.helpers";
import { FunctionNode } from "./variants/FunctionNode";
import { LocationNode } from "./variants/LocationNode";
import { ProductNode } from "./variants/ProductNode";

interface NodeFormProps {
  defaultValues?: FormNodeLib;
}

export const NodeForm = ({ defaultValues = createEmptyFormNodeLibAm() }: NodeFormProps) => {
  const theme = useTheme();
  const { register, handleSubmit, control, setValue, reset } = useForm<FormNodeLib>({ defaultValues });

  const nodeMutation = useCreateNode();
  const hasPrefilled = usePrefilledNodeData(reset);

  const rdsQuery = useGetRds();
  const symbolQuery = useGetSymbols();
  const purposeQuery = useGetPurposes();
  const terminalQuery = useGetTerminals();

  const symbol = useWatch({ control, name: "symbol" });
  const aspect = useWatch({ control, name: "aspect" });
  const nodeTerminals = useWatch({ control, name: "nodeTerminals" });

  return (
    <Box
      as={"form"}
      flex={1}
      display={"flex"}
      flexWrap={"wrap"}
      bgColor={theme.tyle.color.surface.base}
      color={theme.tyle.color.surface.on}
      onSubmit={handleSubmit((data) => nodeMutation.mutate(mapFormNodeLibAmToApiModel(data)))}
    >
      <Box
        as={"fieldset"}
        flex={1}
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"space-between"}
        gap={theme.tyle.spacing.large}
        px={theme.tyle.spacing.large}
        py={theme.tyle.spacing.xl}
        border={0}
      >
        <NodePreview
          img={symbol}
          color={getColorFromAspect(aspect)}
          terminals={getTerminalItemsFromFormData(nodeTerminals, terminalQuery.data)}
        />

        <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.medium}>
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
                <Select
                  {...rest}
                  selectRef={ref}
                  placeholder={textResources.FORMS_NODE_ASPECT_PLACEHOLDER}
                  options={aspectOptions}
                  getOptionLabel={(x) => x.label}
                  onChange={(x) => onChange(x?.value)}
                  value={aspectOptions.find((x) => x.value === value)}
                  isDisabled={hasPrefilled}
                />
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
                    <Flexbox alignItems={"center"} gap={theme.tyle.spacing.xs}>
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

        <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.medium}>
          <Button disabled>Cancel</Button>
          <Button type={"submit"}>Submit</Button>
        </Flexbox>
      </Box>

      <Box
        flex={3}
        display={"flex"}
        flexDirection={"column"}
        gap={theme.tyle.spacing.large}
        px={theme.tyle.spacing.large}
        py={theme.tyle.spacing.xl}
        bgColor={theme.tyle.color.surface.variant.base}
        color={theme.tyle.color.surface.variant.on}
      >
        {aspect === Aspect.Function && <FunctionNode control={control} />}
        {aspect === Aspect.Location && <LocationNode control={control} register={register} />}
        {aspect === Aspect.Product && <ProductNode control={control} />}
      </Box>

      <DevTool control={control} placement={"bottom-right"} />
    </Box>
  );
};
