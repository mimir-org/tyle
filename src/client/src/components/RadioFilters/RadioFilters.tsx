import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { TokenRadioGroup, TokenRadioGroupItem } from "components/Token";
import { useTheme } from "styled-components";
import { Option } from "utils";

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
const RadioFilters = ({ title, filters, onChange, value }: RadioFiltersProps) => {
  const theme = useTheme();
  const inputIsControlled = !!value;

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
      {title && <Text variant={"title-medium"}>{title}</Text>}
      <TokenRadioGroup onValueChange={onChange}>
        {filters.map((x, i) => {
          const conditionalProps: Partial<{ checked: boolean }> = {};
          if (inputIsControlled) {
            conditionalProps.checked = value === x.label;
          }

          return (
            <TokenRadioGroupItem
              key={x.value + i}
              value={x.label}
              onClick={() => onChange(x.label)}
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

export default RadioFilters;
