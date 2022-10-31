import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalTable } from "common/components/terminal/table/TerminalTable";
import { mockNodeTerminalItem } from "common/utils/mocks";

export default {
  title: "Common/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
  },
} as ComponentMeta<typeof TerminalTable>;

const Template: ComponentStory<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
