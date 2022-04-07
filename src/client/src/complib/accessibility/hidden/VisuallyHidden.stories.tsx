import { ComponentMeta, ComponentStory } from "@storybook/react";
import { VisuallyHidden } from "./VisuallyHidden";

export default {
  title: "Accessibility/VisuallyHidden",
  component: VisuallyHidden,
  parameters: {
    viewMode: "docs",
    previewTabs: {
      canvas: { hidden: true },
    },
    docs: {
      source: {
        state: "open",
      },
    },
  },
} as ComponentMeta<typeof VisuallyHidden>;

const Template: ComponentStory<typeof VisuallyHidden> = (args) => <VisuallyHidden {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Some hidden text goes here.",
};
