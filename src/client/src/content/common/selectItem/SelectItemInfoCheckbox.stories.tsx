import { ComponentMeta, ComponentStory } from "@storybook/react";
import { SelectItemInfoCheckbox } from "./SelectItemInfoCheckbox";

export default {
  title: "Content/Common/SelectItem/SelectItemInfoCheckbox",
  component: SelectItemInfoCheckbox,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
    checked: false,
  },
} as ComponentMeta<typeof SelectItemInfoCheckbox>;

const Template: ComponentStory<typeof SelectItemInfoCheckbox> = (args) => <SelectItemInfoCheckbox {...args} />;

export const Default = Template.bind({});
