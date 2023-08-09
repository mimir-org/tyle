import { Option } from "common/utils/getOptionsFromEnum";
import { TokenRadioGroup, TokenRadioGroupItem } from "complib/general";
import { Flexbox } from "@mimirorg/component-library";
import { Text } from "complib/text";
import { useTheme } from "styled-components";

interface RadioFiltersProps {
  title?: string;
  filters: Option<string>[];
  onChange: (value: string) => void;
  value?: string;
}

/**
 * A component which shows the user a selection of filters to choose from.
 * The component can operate in both controlled and uncontrolled mode.
 *
 * @param title text above filters
 * @param filters all filters available to the user
 * @param onChange called when filter value changes
 * @param value allows for controlled-mode of the input
 * @constructor
 */
export const RadioFilters = ({ title, filters, onChange, value }: RadioFiltersProps) => {
  const theme = useTheme();
  const inputIsControlled = !!value;

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
      {title && <Text variant={"title-medium"}>{title}</Text>}
      <TokenRadioGroup onValueChange={onChange}>
        {filters.map((x, i) => {
          const conditionalProps: Partial<{ checked: boolean }> = {};
          if (inputIsControlled) {
            conditionalProps.checked = value === x.value;
          }

          return (
            <TokenRadioGroupItem
              key={x.value + i}
              value={x.value}
              onClick={() => onChange(x.value)}
              {...conditionalProps}
            >
              {x.label}
            </TokenRadioGroupItem>
          );
        })}
      </TokenRadioGroup>
    </Flexbox>
  );
};
