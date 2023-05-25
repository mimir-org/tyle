import { Meta, StoryFn } from "@storybook/react";
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
} as Meta<typeof VisuallyHidden>;

const Template: StoryFn<typeof VisuallyHidden> = (args) => <VisuallyHidden {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Some hidden text goes here.",
};
