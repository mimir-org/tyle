import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Flexbox } from "../../layouts";
import { Icon } from "../../media";
import { Text } from "../../text";
import { Select } from "./Select";

const mockData = [
  { label: "Item A", value: "Value A" },
  { label: "Item B", value: "Value B" },
  { label: "Item C", value: "Value C" },
  { label: "Item D", value: "Value D" },
  { label: "Item E", value: "Value E" },
];

export default {
  title: "Inputs/Select",
  component: Select,
} as ComponentMeta<typeof Select>;

const Template: ComponentStory<typeof Select> = (args) => <Select {...args} />;

export const Default = Template.bind({});
Default.args = {
  options: mockData,
};

export const WithComponentOptions = () => (
  <Select
    options={mockData}
    getOptionLabel={(x) => x.label}
    getOptionValue={(x) => x.value}
    formatOptionLabel={(x) => (
      <Flexbox alignItems={"center"} gap={"8px"}>
        <Icon src={LibraryIcon} />
        <Text>{x.label}</Text>
      </Flexbox>
    )}
  />
);
