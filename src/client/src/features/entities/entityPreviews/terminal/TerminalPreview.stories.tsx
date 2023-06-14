import { faker } from "@faker-js/faker";
import { Meta, StoryFn } from "@storybook/react";
import { TerminalPreview } from "features/entities/entityPreviews/terminal/TerminalPreview";

export default {
  title: "Features/Common/Terminal/TerminalPreview",
  component: TerminalPreview,
} as Meta<typeof TerminalPreview>;

const Template: StoryFn<typeof TerminalPreview> = (args) => <TerminalPreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Terminal",
  color: faker.color.rgb(),
};
