import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTerminalItem } from "../../../../../../utils/mocks";
import { TerminalTable } from "./TerminalTable";

export default {
  title: "Content/Home/About/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockTerminalItem()),
  },
} as ComponentMeta<typeof TerminalTable>;

const Template: ComponentStory<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
