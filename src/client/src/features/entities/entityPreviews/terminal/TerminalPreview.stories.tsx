import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalPreview } from "features/entities/entityPreviews/terminal/TerminalPreview";

export default {
  title: "Features/Common/Terminal/TerminalPreview",
  component: TerminalPreview,
} as ComponentMeta<typeof TerminalPreview>;

const Template: ComponentStory<typeof TerminalPreview> = (args) => <TerminalPreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Terminal",
  color: faker.color.rgb(),
};
